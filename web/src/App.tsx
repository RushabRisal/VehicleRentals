import { useState } from "react"

function App() {
  const [value,SetValue] = useState<any>("");
  const fetchHello = () =>{
      fetch("http://localhost:5257/")
        .then(res => res.text())
        .then(data => SetValue(data));
      console.log(value);
  }
  return (
    <div>
        {value}
        <button onClick={fetchHello}>click me</button>
    </div>
  )
}

export default App
