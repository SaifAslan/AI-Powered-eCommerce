from dotenv import load_dotenv
import os
from pinecone import Pinecone, ServerlessSpec, Index
from langchain_openai import OpenAIEmbeddings
from langchain_openai import ChatOpenAI
from langchain_pinecone import PineconeVectorStore  
from langchain.prompts import PromptTemplate
from langchain.chains.conversational_retrieval.base import ConversationalRetrievalChain

# Load environment variables
load_dotenv()
print(os.environ["PINECONE_API_KEY"])
# Initialize Pinecone instance
pc = Pinecone(
    api_key=os.environ["PINECONE_API_KEY"]
)
print(pc.list_indexes().names())
# Check if the index already exists, otherwise create it
index_name = os.environ["INDEX_NAME"]
if index_name not in pc.list_indexes().names():
    pc.create_index(
        name=index_name,
        dimension=1536,
        metric='cosine',
        spec=ServerlessSpec(
            cloud='aws',
            region='us-east-1'
        )
    )
else:
    print(f"Index '{index_name}' already exists. Skipping index creation.")

# Function to initialize OpenAI embeddings
def init_embeddings() -> OpenAIEmbeddings:
    return OpenAIEmbeddings(model="text-embedding-ada-002")

# Function to initialize the Pinecone vector store
def init_vector_store(embeddings: OpenAIEmbeddings, index_name: str) -> PineconeVectorStore:
    # Initialize the Pinecone index
    index = Index(index_name, host = "https://e-commerce-ilvph1d.svc.aped-4627-b74a.pinecone.io")
    
    # Initialize Pinecone vector store from LangChain and connect it with embeddings
    return PineconeVectorStore(index_name=index_name, embedding=embeddings)

# Function to initialize the chat model
def init_chat() -> ChatOpenAI:
    return ChatOpenAI(model="gpt-4o-mini", temperature=0.7)

# Function to initialize the conversational retrieval chain
def init_conversational_retrieval_chain(chat: ChatOpenAI, vector_store: PineconeVectorStore) -> ConversationalRetrievalChain:
    # No need for embedding_function separately; it's handled by the vector store
    prompt_template = """
    You are a helpful assistant for an online clothing shop. You have access to the following documents.
    If the answer to the user's question is not in the documents, respond with: "I'm not sure, but let me help you find out.".
    Use the context below to help answer the question:
    {chat_history}

    Documents:
    {context}

    Use both the conversation and documents to answer the question:
    {question}
    """
    PROMPT = PromptTemplate(input_variables=["chat_history","context", "question"], template=prompt_template)

    # return  LLMChain(llm=chat, prompt=PROMPT)

    return ConversationalRetrievalChain.from_llm(
        llm=chat, 
        retriever=vector_store.as_retriever(),  # Use Pinecone retriever from LangChain
        return_source_documents=True,
        # prompt_template=PROMPT
    )

def custom_prompt(context, question,chat_history= []):
    prompt_template = """
    You are a helpful assistant for an online clothing shop. You have access to the following documents.
    If the answer to the user's question is not in the documents, respond with: "I'm not sure, but let me help you find out.".
    Use the context below to help answer the question:
    {chat_history}

    Documents:
    {context}

    Use both the conversation and documents to answer the question:
    {question}
    """
    prompt = prompt_template.format(chat_history=chat_history, context=context, question=question)
    return prompt


# Function to run the LLM
def run_llm(query: str, chat_history: list = []) -> dict:
    # Initialize embeddings and vector store
    embeddings = init_embeddings()
    docsearch = init_vector_store(embeddings, index_name)
    
    # Initialize the chat model
    chat = init_chat()
    
    # Initialize the conversational retrieval chain
    qa = init_conversational_retrieval_chain(chat, docsearch)

    documents = docsearch.similarity_search(query, k=5)  # Retrieve relevant documents
    context = " ".join([doc.page_content for doc in documents])  # Join retrieved documents as context

    prompt = custom_prompt(context, query,chat_history)  # Generate the custom prompt
    
    # Run the chain with the custom prompt
    result = qa({"question": prompt,         "chat_history": chat_history  # The ongoing conversation history, default to empty list
})
    # Run the conversational chain with the raw query
    # result = qa({"question": query, "chat_history": chat_history})
    if not result.get("source_documents"):
            return {"answer": "I'm not sure. Please try rephrasing your question or provide more details."}
    return result

if __name__ == "__main__":
    print(index_name)
    # Example query
    print(run_llm(query="find me products are red")['answer'])
