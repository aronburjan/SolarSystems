

function updateComponentTable() {
    let address = "https://localhost:7032/api/Components";
    let counter = 0;
    //send GET request
    fetch(address, {
        method: "GET"
    })
        .then(response => {
            currentStatus = response.status;
            console.log('response.status: ', response.status);
            if (response.ok) {
                return response.json();
            }
        })
        .then(data => {
            //clear table
            document.getElementById('componentTable').innerHTML = '';
            //create new table row
            data.forEach(item => {
                let table = document.getElementById("componentTable");
                let row = table.insertRow(counter);
                let cellID = row.insertCell(0);
                let cellName = row.insertCell(1);
                let cellPrice = row.insertCell(2);
                let cellQuantity = row.insertCell(3);
                let cellSelected = row.insertCell(4);
                //fill out cell data
                cellID.innerHTML = item.id;
                cellName.innerHTML = item.componentName;
                cellPrice.innerHTML = item.price;
                cellQuantity.innerHTML = '<input type="number" id="quantity' + item.id + '" name="quantity' + item.id + '" min="0" max="' + item.maxStack + '" disabled>';
                cellSelected.innerHTML = '<input type="checkbox" id="selected' + item.id + '" name="selected' + item.id + '" onclick="enableQuantity(' + item.id + ')">';
                counter++;
            });
        })
        .catch((err) => {
            console.log(err);
        });
}

function enableQuantity(id) {
    let checkbox = document.getElementById('selected' + id);
    let quantityInput = document.getElementById('quantity' + id);
    quantityInput.disabled = !checkbox.checked;
}


function updateTable() {
    let address = "https://localhost:7032/api/Projects";
    let counter = 0;
    //send GET request
    fetch(address, {
        method: "GET"
    })
        .then(response => {
            currentStatus = response.status;
            console.log('response.status: ', response.status);
            if (response.ok) {
                return response.json();
            }
        })
        .then(data => {
            //clear table
            document.getElementById('projectTable').innerHTML = '';
            //create new table row
            data.forEach(item => {
                let table = document.getElementById("projectTable");

                let row = table.insertRow(counter);

                let cellPlace = row.insertCell(0);
                let cellCustomer = row.insertCell(1);
                let cellDescription = row.insertCell(2);
                let cellLtime = row.insertCell(3);
                let cellLrate = row.insertCell(4);
                let cellId = row.insertCell(5);

                //fill out cell data
                cellPlace.innerHTML = item.projectLocation;
                cellCustomer.innerHTML = item.customerName;	
                cellDescription.innerHTML = item.projectDescription;
                cellLtime.innerHTML = item.laborTime;
                cellLrate.innerHTML = item.hourlyLaborRate;
                cellId.innerHTML = item.id;

                // add click event listener to row
                row.addEventListener("click", () => selectRow(row));

                counter++;
            });
        })
        .catch((err) => {
            console.log(err);
        });
}
function selectRow(row) {
    // reset background color of all rows
    const rows = document.querySelectorAll("#projectTable tr");
    rows.forEach((r) => (r.style.backgroundColor = ""));

    // highlight selected row
    row.style.backgroundColor = "yellow";

    // get data from cells of selected row
    const place = row.cells[0].textContent;
    const customer = row.cells[1].textContent;
    const description = row.cells[2].textContent;
    const lTime = row.cells[3].textContent;
    const lRate = row.cells[4].textContent;
    const id = row.cells[5].textContent;

    // display data in selectedRow div
    document.getElementById("selectedRow").innerHTML = id;

    //addComponentsToProject(id);
}


function displaySelectedRowData() {
    // Get the selected row
    let selectedRow = document.querySelector(".selected");

    // Get the data from the selected row
    let place = selectedRow.cells[0].innerHTML;
    let customer = selectedRow.cells[1].innerHTML;
    let description = selectedRow.cells[2].innerHTML;
    let laborTime = selectedRow.cells[3].innerHTML;
    let hourlyLaborRate = selectedRow.cells[4].innerHTML;
    let id = selectedRow.cells[5].innerHTML;

    // Create a new <div> element to display the data
    let div = document.createElement("div");
    div.innerHTML = "Place: " + place + "<br>" +
        "Customer: " + customer + "<br>" +
        "Description: " + description + "<br>" +
        "Labor Time: " + laborTime + "<br>" +
        "Hourly Labor Rate: " + hourlyLaborRate;
        "Id: " + id;

    // Add the new <div> element to the HTML page
    let container = document.querySelector(".container");
    container.appendChild(div);
}

function addComponentsToProject() {
	let projectId = document.getElementById("selectedRow").innerHTML;
    // get all selected checkboxes and corresponding quantity inputs
    let selectedComponents = [];
    let checkboxes = document.querySelectorAll("input[type='checkbox']");
    checkboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            let id = checkbox.id.substring(8); // get component id from checkbox id
            let quantityInput = document.getElementById('quantity' + id);
            let quantity = quantityInput.value;
            selectedComponents.push({ id: id, quantity: quantity });
        }
    });

    // send POST request for each selected component
    selectedComponents.forEach((component) => {
        let componentId = component.id;
        let componentQuantity = component.quantity;
        let address = "https://localhost:7032/api/Projects/" + componentId + "/" + componentQuantity + "/" + projectId;

        fetch(address, {
            method: "POST"
        })
            .then(response => {
                currentStatus = response.status;
                console.log('response.status: ', response.status);
                if (response.ok) {
                    console.log("Component added to project successfully.");
                } else {
                    console.log("Failed to add component to project.");
                }
            })
            .catch((err) => {
                console.log(err);
            });
    });
}
