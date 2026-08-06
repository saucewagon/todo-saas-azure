const API_URL = "http://localhost:5248";

export async function getTodos() {
    const response = await fetch(`${API_URL}/api/todos`);

    return response.json();
}

export async function addTodo(title: string) {
    const response = await fetch(`${API_URL}/api/todos`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            title,
        }),
    });

    return response.json();
}