    function addProject() 
		{
			const place 		= document.getElementById("placeInput").value;
			const customer 		= document.getElementById("customerNameInput").value;
			const description 	= document.getElementById("projectDescriptionInput").value;
			const ltime 		= document.getElementById("laborTimeInput").value;
			const lrate = document.getElementById("hourlyLaborRateInput").value;
			//const totalprice = document.getElementById("totalPriceInput").value;
		const address = "https://localhost:7032/api/Projects/" + description + "/" + place + "/" + customer + "/" + lrate + "/" + ltime;
			//send POST request
		console.log("Adding Project:", place, customer, description, ltime, lrate, address);
			fetch(address, 
				{
					method: "POST",
					headers: 
						{
						"Content-Type": "application/json",
						},/*
					body: JSON.stringify
						(
							{
							projectLocation: place,
							customerName: customer,
							projectDescription: description,
							laborTime: ltime,
							hourlyLaborRate: lrate,
							}
						)*/
				}
				)
			.then(response => 
				{
				console.log('response.status: ', response.status);
				console.log(response);
				if (!response.ok) 
					{
					console.log('Response not ok');
					//document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
					}
				else 
					{
					updateTable();
					}
				}
				)
		}


		function updateTable(){
			let address = "https://localhost:7032/api/Projects";
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
					let table = document.getElementById("projectTable");
					
					let row 				= table.insertRow(counter);
					
					let cellID 				= row.insertCell(0);
					let cellPlace			= row.insertCell(1);
					let cellCustomer		= row.insertCell(2);
					let cellDescription 	= row.insertCell(3);
					let cellLtime 			= row.insertCell(4);
					let cellLrate 			= row.insertCell(5);
					let cellStatus = row.insertCell(6);
					let cellTotalPrice = row.insertCell(7);
					
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

function doPriceCalc(){
	const projectID = getSelectedID();
	const address = "https://localhost:7032/api/Projects/" + projectID;
	
	fetch(address)
		.then(response => response.json())
		.then(data => {
			if ((data.currentStatus != "Draft") && (data.currentStatus != "Wait")) {
				console.log("selected project is not Draft/Wait (or no project is selected)");
				document.getElementById("errorMessage").innerHTML = "To do price calculation, select a project that is in Draft or Wait status!";
			} else {
				
				const estimateAddress = "https://localhost:7032/api/Projects/estimate/" + projectID;

				fetch(estimateAddress, {
					method: 'GET'
				})
				.then(() => {
					console.log("Price estimated for project " + projectID);
					updateTable();
				})
				.catch(error => {
					console.error('Error:', error);
				});
				
			}
		})
		.catch(error => {
			console.error('Error:', error);
		});
}

function closeProject(status){
	const projectID = getSelectedID();
	const address = "https://localhost:7032/api/Projects/" + projectID;
	
	fetch(address)
		.then(response => response.json())
		.then(data => {
			if (data.currentStatus != "InProgress") {
				console.log("selected project is not InProgress (or no project is selected)");
				document.getElementById("errorMessage").innerHTML = "To close a project, select a project that is in InProgress status!";
			} else {
				
				if((status == "Completed") || (status=="Failed")){
					const closeAddress = "https://localhost:7032/api/Projects/set/status/" + projectID + "/" + status;
					fetch(closeAddress, {
						method: 'PUT'
					})
					.then(() => {
						console.log("Closed project " + projectID + " with status " + status);
						updateTable();
					})
					.catch(error => {
						console.error('Error:', error);
					});
					
				}
				
			}
		})
		.catch(error => {
			console.error('Error:', error);
		});
		

}