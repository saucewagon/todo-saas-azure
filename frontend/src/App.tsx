import { useEffect, useState } from "react";
import { addTodo, getTodos } from "./api";

type Todo = {
    id: string;
    title: string;
    completed: boolean;
};

function App() {
    const [todos, setTodos] = useState<Todo[]>([]);
    const [title, setTitle] = useState("");

    async function loadTodos() {
        const data = await getTodos();
        setTodos(data);
    }

    async function createTodo() {
        await addTodo(title);
        setTitle("");
        loadTodos();
    }

    useEffect(() => {
        loadTodos();
    }, []);

    return (
        <div>
            <h1>Todo List</h1>

            <input
                value={title}
                onChange={(e) => setTitle(e.target.value)}
            />

            <button onClick={createTodo}>
                Add
            </button>

            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        {todo.title}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;