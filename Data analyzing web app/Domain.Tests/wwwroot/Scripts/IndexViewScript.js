const Machines = document.querySelector("#Machines");

async function GetMachinesAsync()
{
    const response = await fetch("/getMachines");
    if(response.ok)
    {
        Machines.innerHTML = await response.text();  
    }
    else
    {
        Machines.innerHTML = "<h2>Failed to load machines</h2>";
    }
}

document.body.onload = GetMachinesAsync;
document.querySelector("#RefreshButton").addEventListener("click", GetMachinesAsync);