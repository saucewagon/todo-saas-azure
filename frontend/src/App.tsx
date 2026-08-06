import { useEffect, useState } from "react";
import { addTodo, deleteTodo, getTodos, updateTodo } from "./api";
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
    async function removeTodo(id: string) {
      await deleteTodo(id);
      loadTodos();
    }
    async function toggleTodo(todo: Todo) {
      await updateTodo(todo.id,todo.title,!todo.completed);
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
    <input
        type="checkbox"
        checked={todo.completed}
        onChange={() => toggleTodo(todo)}
    />

    <span
        style={{
            textDecoration: todo.completed
                ? "line-through"
                : "none",
            marginLeft: "10px",
        }}
    >
        {todo.title}
    </span>

    <button
        onClick={() => removeTodo(todo.id)}
        style={{ marginLeft: "10px" }}
    >
        Delete
    </button>
</li>
                ))}
            </ul>
        </div>
    );
}

export default App;