function updateTable(){
			let address = "https://localhost:7032/status/Scheduled";
			let counter = 0;
			//send GET request
			fetch(address, {
				method: "GET"
			})
			.then(response => {
				currentStatus = response.status;
				console.log('response.status: ', response.status);
			if (response.ok){
				return response.json(); 
			}
			})
			.then(data => {
				//clear table
				document.getElementById('projectTable').innerHTML = '';
				//create new table row
				data.forEach(item =>{
					//list Scheduled projects
					let table = document.getElementById("projectTable");
						let row 				= table.insertRow(counter);
						
						let cellID 				= row.insertCell(0);
						let cellPlace			= row.insertCell(1);
						let cellCustomer		= row.insertCell(2);
						let cellDescription 	= row.insertCell(3);
						let cellLtime 			= row.insertCell(4);
						let cellLrate 			= row.insertCell(5);
						let cellStatus 			= row.insertCell(6);
						let cellTotalPrice 		= row.insertCell(7);
						
						//fill out cell data
						cellID.innerHTML 			= item.id;
						cellPlace.innerHTML 		= item.projectLocation;
						cellCustomer.innerHTML 		= item.customerName;
						cellDescription.innerHTML 	= item.projectDescription;
						cellLtime.innerHTML 		= item.laborTime;
						cellLrate.innerHTML 		= item.hourlyLaborRate;
						cellStatus.innerHTML = item.currentStatus;
						cellTotalPrice.innerHTML = item.totalPrice;
						
						//add event listener for selection
						row.addEventListener("click", () => selectRow(row));
					counter++;
					
				}); 
			})
			.catch((err) => {
				console.log(err);
			});	
		}
	
/*	
async function planRoute(){
	output = ""; 
	
			
	//the following code might become obsolete with the existence of /api/projects/take/compoents/for/{id}
	for (let s = 1; s <= 5; s++) {
	  for (let c = 1; c <= 5; c++) {
		for (let r = 1; r <= 4; r++) {
		  const address = "https://localhost:7032/api/Containers/" + r + "/" + c + "/" + s;
		  // send GET request
		  console.log('sending request to ' + address);
		  try {
			const response = await fetch(address, {
			  method: "GET"
			});

			console.log('response.status: ', response.status);
			console.log(response);

			if (!response.ok) {
			  console.log('Response not ok');
			  // document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
			} else {
			  const data = await response.json();
			  if (data.quantityInContainer > 0){
			  output +=
				data.containerNumber + ". polc, " +
				data.containerColumn + ". oszlop, " +
				data.containerRow + ". sor: " +
				data.quantityInContainer + "x " +
				data.component + " <br>";
			  }

			}
		  } catch (error) {
			console.log(error);
		  }
		}
	  }
	}

	console.log(output);
	document.getElementById('routeDisplay').innerHTML = 'selected project: ' + getSelectedID() + ' <br>' + output;
	
}
*/

function selectRow(r){
	const table = document.getElementById("projectTable");
    const rows = table.getElementsByTagName("tr");
	
	for (let i = 0; i < rows.length; i++) {
          rows[i].classList.remove("selected");
    }
	console.log(r);
	r.classList.add("selected")
}

function getSelectedID() {
  const table = document.getElementById("projectTable");
  const rows = table.getElementsByTagName("tr");
  
  for (let i = 0; i < rows.length; i++) {
    if (rows[i].classList.contains("selected")) {
      const cells = rows[i].getElementsByTagName("td");
      return cells[0].innerHTML;
    }
  }
  
  return null; // Return null if no selected row
}

async function planRoute(){
	const projectID = getSelectedID();
	const output = document.getElementById("routeDisplay");
	//reset output
	output.innerHTML = "";
	
	if (projectID == null){
		output.innerHTML = "Please select a project!"
	} else {

		//display required components name + amount
		output.innerHTML += "Required components: <br>";
		try {
			const response = await fetch("https://localhost:7032/api/projectComponents/" + projectID);
			const projectComponents = await response.json();

			for (const projectComponent of projectComponents) {
				const componentID = projectComponent.componentId;
				const componentResponse = await fetch("https://localhost:7032/api/components/" + componentID);
				const component = await componentResponse.json();

				output.innerHTML += component.componentName + " x " + projectComponent.quantity + " <br>";
				
			}
		} catch (error) {
			console.error('Error:', error);
		}

		//display route
		output.innerHTML += "Route: <br>";
		const routeAddress = "https://localhost:7032/api/Projects/project/component/info/" + projectID;
		
			fetch(routeAddress, {
					method: "GET"
				})
				.then(response => {
					currentStatus = response.status;
					console.log('response.status: ', response.status);
				if (response.ok){
					return response.json(); 
				}
				})
				.then(data => {

					data.forEach(item =>{
						output.innerHTML += 
							"Shelf " + item.containerNumber + ", " +
							"Column " + item.containerColumn + ", " +
							"Row " + item.containerRow + ": " +
							item.component.componentName + " <br>";
					}); 
					//display button to take the items
					output.innerHTML += '<button onclick="takeItems()">Finished Collecting Components</button>';
				})
				.catch((err) => {
					console.log(err);
				});

	}
}

function takeItems(){
	const projectID = getSelectedID();
	//take components from warehouse
	const address = "https://localhost:7032/api/Projects/take/components/for/" + projectID;
		fetch(address, {
				method: "POST"
			})
			.then(response => {
				currentStatus = response.status;
				console.log('response.status: ', response.status);
			if (response.ok){
				//set project status to inProgress
				
				const statusAddress = "https://localhost:7032/api/Projects/set/status/" + projectID + "/inProgress";
					fetch(closeAddress, {
						method: 'PUT'
					})
					.then(() => {
						console.log("set project  " + projectID + "  to inProgress ");
					})
					.catch(error => {
						console.error('Error:', error);
					});
				
			}
			})
			.catch((err) => {
				console.log(err);
			});

	updateTable();
}