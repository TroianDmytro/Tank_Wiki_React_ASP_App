import { useEffect, useState } from 'react';
import './App.css';

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

interface Tanks {
    id: number,
    name: string,
    tier: number,
    hitPoints: number,
    status: boolean,
    price: number,
    description: string,
    typeId: number,
    armorId: number,
    crew: string,
    nationId:number
}
function App() {
    //const [forecasts, setForecasts] = useState<Forecast[]>();
    const [getTanks, setTanks] = useState<Tanks[]>();

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
                    <th>Type Id</th>
                    <th>Armor Id</th>
                    <th>Crew</th>
                    <th>Nation Id</th>
                </tr>
            </thead>
            <tbody>
                {getTanks.map(tank =>
                    <tr key={tank.id}>
                        <td>{tank.name}</td>
                        <td>{tank.tier}</td>
                        <td>{tank.hitPoints}</td>
                        <td>{tank.status}</td>
                        <td>{tank.price}</td>
                        <td>{tank.description}</td>
                        <td>{tank.typeId}</td>
                        <td>{tank.armorId}</td>
                        <td>{tank.crew}</td>
                        <td>{tank.nationId}</td>
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
        const response = await fetch('Tanks');
        const data = await response.json();
        //setForecasts(data);MN
        setTanks(data);
    }
}

export default App;