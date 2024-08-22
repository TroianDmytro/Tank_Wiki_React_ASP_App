import { useEffect, useState } from 'react';
import axios from 'axios';
import './App.css';
import ContainerImg from "./components/ContainerImg/ContainerImg"

//interface Forecast {
//    date: string;
//    temperatureC: number;
//    temperatureF: number;
//    summary: string;
//}

interface Tanks {
    tankId?: number,
    name?: string,
    tier?: number,
    hitPoints?: number,
    status?: boolean,
    price?: number,
    description?: string,
    typeId?: number,
    armorId?: number,
    crew?: string,
    nation?: string
}
function App() {
    //const [forecasts, setForecasts] = useState<Forecast[]>();
    const [getTanks,setTanks] = useState<Tanks[]>();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = getTanks === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Tier</th>
                    <th>HitPoints</th>
                    <th>Status</th>
                    <th>Price</th>
                    <th>Description</th>
                    <th>Crew</th>
                    <th>Img</th>
                </tr>
            </thead>
            <tbody>
                {getTanks.map(tank =>
                    <tr key={tank.tankId}>
                        <td>{tank.tankId}</td>
                        <td>{tank.name}</td>
                        <td>{tank.tier}</td>
                        <td>{tank.hitPoints}</td>
                        <td>{tank.status}</td>
                        <td>{tank.price}</td>
                        <td>{tank.description}</td>
                        <td>{tank.armorId}</td>
                        <td>{tank.crew}</td>
                        <td>{<ContainerImg/>}</td>
                    </tr>
                    
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        //const response = await fetch('Tanks');
        //const data = await response.json();
        //setForecasts(data);MN
        const res = (await axios.get("https://localhost:7032/Tanks"));
        console.log(res);
        setTanks(res.data);
    }
}

export default App;