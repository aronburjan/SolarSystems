function updateTable(){
			let address = "https://localhost:7032/status/new";
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