import { useState } from "react"

function App() {
  const [value,SetValue] = useState<any>([]);
  const fetchHello = () =>{
      fetch("http://localhost:3000/api/v1/rental/vehicles",{
        credentials:"include",
        method:"GET",
      })
        .then(res => res.json())
        .then(data => SetValue(data));
      console.log(value);
  }
  return (
    <div>
       
        <button onClick={fetchHello}>click me</button>
        
    </div>
  )
}

export default App
