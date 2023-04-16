 // Toggle between add and edit mode
      function toggleMode() {
        const modeSwitch = document.getElementById("modeSwitch");
        const addMode = document.getElementById("addMode");
        const editMode = document.getElementById("editMode");
        const form = document.getElementById("componentForm");
        if (modeSwitch.checked) {
          // Edit mode
          addMode.style.display = "none";
          editMode.style.display = "block";
          //form.setAttribute("onsubmit", "event.preventDefault(); editComponent()");
		  document.getElementById("ModeLabel").innerHTML = "Edit";
        } else {
          // Add mode
          addMode.style.display = "block";
          editMode.style.display = "none";
          //form.setAttribute("onsubmit", "event.preventDefault(); addComponent()");
		  document.getElementById("ModeLabel").innerHTML = "Add";
        }
      }

    function addComponent() {
        const name = document.getElementById("nameInput").value;
        const price = document.getElementById("priceInput").value;
        const max = document.getElementById("maxInput").value;
		const address = "https://localhost:7032/api/Components"
		//send POST request
        console.log("Adding component:", name, price, max);
		fetch(address, {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify({
				componentName: name,
				maxStack: max,
				price: price})
		})
		.then(response => {
			console.log('response.status: ', response.status);
			console.log(response);
			if (!response.ok) {
				console.log('Response not ok');
				document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
			} else {
				updateTable();
			}
		})
		
	}

      function editComponent() {
        const id = document.getElementById("idInput").value;
        const price = document.getElementById("newPriceInput").value;
		const address = "https://localhost:7032/api/Components/" + id + "/" + price;
        console.log("Editing component:", id, price);
		//send PUT request
		fetch(address, {
			method: "PUT"
		})
		.then(response => {
			console.log('response.status: ', response.status);
			console.log(response);
			if (!response.ok) {
				console.log('Response not ok');
				document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
			} else {
				updateTable();
			}
		})
	  }
	  
		function updateTable(){
			let address = "https://localhost:7032/api/Components";
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
				document.getElementById('componentTable').innerHTML = '';
				//create new table row
				data.forEach(item =>{
					let table = document.getElementById("componentTable");
					let row = table.insertRow(counter);
					let cellID = row.insertCell(0);
					let cellName = row.insertCell(1);
					let cellPrice = row.insertCell(2);
					let cellMax = row.insertCell(3);
					//fill out cell data
					cellID.innerHTML = item.id;
					cellName.innerHTML = item.componentName;
					cellPrice.innerHTML = item.price;
					cellMax.innerHTML = item.maxStack;
					counter++;
					}); 
			})
			.catch((err) => {
				console.log(err);
			});	
		}
	