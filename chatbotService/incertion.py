import os
import requests 
from dotenv import load_dotenv
from pinecone import Pinecone
from openai import OpenAI

client = OpenAI()


client.api_key = os.environ['OPENAI_API_KEY']

pc = Pinecone(api_key=os.environ['PINECONE_API_KEY'])

index = pc.Index(os.environ["INDEX_NAME"])


# Load environment variables
load_dotenv()




def get_variant_embedding(product, variant):
    # Combining product details and variant-specific information
    description = (f"{product['name']}: {product['description']}, Price: {product['price']}, "
                   f"Brand: {product['brand']['name']}, Category: {product['category']['name']}, "
                   f"Size: {variant['size']['name']}, Colour: {variant['colour']['name']}, SKU: {variant['sku']}")
    
    response = client.embeddings.create(input=description, model="text-embedding-ada-002")
    return response.data[0].embedding

def store_variants_in_pinecone(product):
    for variant in product['variants']:
        embedding = get_variant_embedding(product, variant)
        text_content = (f"{product['name']} {product['description']}, Brand: {product['brand']['name']}, "
                        f"Category: {product['category']['name']}, Size: {variant['size']['name']}, "
                        f"Colour: {variant['colour']['name']}, SKU: {variant['sku']}")

    
        metadata = {
            'ProductId': product['id'],
            'ProductName': product['name'],
            'Description': product['description'],
            'Price': product['price'],
            'Brand': product['brand']['name'],
            'Category': product['category']['name'],
            'SubCategory': product['subCategory']['name'],
            'Gender': product['gender']['name'],
            'VariantId': variant['id'],
            'Size': variant['size']['name'],
            'Colour': variant['colour']['name'],
            'SKU': variant['sku'],
            'Materials': ', '.join([f"{m['percentage']}% {m['name']}" for m in product['materials']]),
            'text': text_content
        }
        
        index.upsert([(str(variant['id']), embedding, metadata)])


if __name__ == "__main__":
    base_url = os.environ["PRODUCTS_URL"]
    base_params = {}
    response = requests.get(base_url, params=base_params)
    products = response.json()

    for product in products:
        store_variants_in_pinecone(product)



