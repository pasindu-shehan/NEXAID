
# from fastapi import FastAPI
# from sentence_transformers import SentenceTransformer

# app = FastAPI()

# # Load the AI model once when the service starts
# model = SentenceTransformer("all-MiniLM-L6-v2")


# @app.get("/")
# def home():
#     return {
#         "message": "Humanitarian Duplicate Detection AI Service Running"
#     }


# @app.post("/generate-embedding")
# def generate_embedding(data: dict):

#     description = data.get("description", "")

#     if not description.strip():
#         return {"error": "Description is required"}

#     embedding = model.encode(description).tolist()

#     return {
#         "embedding": embedding
#     }


# from fastapi import FastAPI
# from sentence_transformers import SentenceTransformer

# app = FastAPI()

# # Load the AI model once when the service starts
# model = SentenceTransformer("all-MiniLM-L6-v2")


# @app.get("/")
# def home():
#     return {
#         "message": "Humanitarian Duplicate Detection AI Service Running"
#     }


# @app.post("/generate-embedding")
# def generate_embedding(data: dict):

#     # Receive combined description and location from C#
#     embedding_text = data.get("description", "")

#     if not embedding_text.strip():
#         return {"error": "Description is required"}

#     # Generate 384-dimensional embedding
#     embedding = model.encode(embedding_text).tolist()

#     return {
#         "embedding": embedding
#     }


from fastapi import FastAPI
from sentence_transformers import SentenceTransformer

app = FastAPI()

model = SentenceTransformer("all-MiniLM-L6-v2")


@app.get("/")
def home():
    return {
        "message": "Humanitarian Duplicate Detection AI Service Running"
    }


@app.post("/generate-embedding")
def generate_embedding(data: dict):

    embedding_text = data.get("description", "")

    if not embedding_text.strip():
        return {"error": "Description is required"}

    embedding = model.encode(
        embedding_text,
        normalize_embeddings=True
    ).tolist()

    return {
        "embedding": embedding
    }