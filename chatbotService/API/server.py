from flask import Flask, request, jsonify
import traceback
from core import run_llm  # Replace with your LLM module filename
from insertion import fetch_store_products # Replace with your data incertion module filename


app = Flask(__name__)

@app.route('/chat', methods=['POST'])
def chat():
    try:
        data = request.get_json()
        query = data.get('query')
        chat_history = data.get('chat_history', [])

        if not query:
            return jsonify({"error": "Query is required"}), 400

        result = run_llm(query, chat_history)
        return jsonify(result)

    except Exception as e:
        traceback.print_exc()  # Log the error for debugging
        return jsonify({"error": "Internal server error"}), 500

@app.route('/fetch_and_store', methods=['POST'])
def fetch_and_store():
    try:
        fetch_store_products()  # Call the function to fetch and store products
        return jsonify({"message": "Products fetched and stored successfully."}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(debug=True)