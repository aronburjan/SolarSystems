    function addProject() 
		{
			const place 		= document.getElementById("placeInput").value;
			const customer 		= document.getElementById("customerNameInput").value;
			const description 	= document.getElementById("projectDescriptionInput").value;
			const ltime 		= document.getElementById("laborTimeInput").value;
			const lrate 		= document.getElementById("hourlyLaborRateInput").value;
			const address 		= "https://localhost:7032/createNewProject"
			//send POST request
			console.log("Adding Project:", place, customer, description, ltime, lrate, address);
			fetch(address, 
				{
					method: "POST",
					headers: 
						{
						"Content-Type": "application/json",
						},
					body: JSON.stringify
						(
							{
							place: place,
							customer: customer,
							description: description,
							ltime: ltime,
							lrate: lrate,
							}
						)
				}
				)
			.then(response => 
				{
				console.log('response.status: ', response.status);
				console.log(response);
				if (!response.ok) 
					{
					console.log('Response not ok');
					document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
					}
				else 
					{
					updateProjectable();
					}
				}
				)
		}


		function updateProjectable(){
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
					
					let cellPlace			= row.insertCell(0);
					let cellCustomer		= row.insertCell(1);
					let cellDescription 	= row.insertCell(2);
					let cellLtime 			= row.insertCell(3);
					let cellLrate 			= row.insertCell(4);
					
					//fill out cell data
					cellPlace.innerHTML 		= item.place;
					cellCustomer.innerHTML 		= item.customer;
					cellDescription.innerHTML 	= item.description;
					cellLtime.innerHTML 		= item.ltime;
					cellLrate.innerHTML 		= item.lrate;
					
					counter++;
					}); 
				})
			.catch((err) => {
				console.log(err);
			});	
		}
	
