document.querySelector("#GetAggregateMachineDatasButton").addEventListener("click", async () =>
{
    const machineId = document.querySelector("#MachineID").textContent;
    const startDate = document.querySelector("#StartDateInput").value;
    const howManyDatesForward = document.querySelector("#HowManyDatesForward").value;
    if(startDate == "")
    {
        alert("Fill start date.");
        return;
    }
    else if(howManyDatesForward == "")
    {
        alert("Fill how many dates forward.");
        return;
    }

    const response = await fetch("/getAggregatedMachineDatas/"
        + machineId + "/" + startDate + "/" + howManyDatesForward);

    if(response.ok)
    {
        document.querySelector("#AggregatedMachineDatas").innerHTML = await response.text();
    }
    else
    {
        alert("Error occured. Code: " + response.status
            + ". Message: " + await response.text());
    }
});