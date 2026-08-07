//const API_URL = "http://localhost:5248";
const API_URL = import.meta.env.VITE_API_URL;

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
export async function deleteTodo(id: string) {
    await fetch(`${API_URL}/api/todos/${id}`, {
        method: "DELETE",
    });
}
export async function updateTodo(
    id: string,
    title: string,
    completed: boolean
) {
    await fetch(`${API_URL}/api/todos/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            title,
            completed,
        }),
    });
}