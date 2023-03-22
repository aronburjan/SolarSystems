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

        console.log("Adding component:", name, price, max);
		fetch(address, {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify({
				componentName: name,
				maxQuantity: max,
				price: price})
		})
		.then(response => {
			console.log('response.status: ', response.status);
			console.log(response);
			if (!response.ok) {
				console.log('Response not ok');
				document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
			} 
		})
		//updateTable();
	}

      function editComponent() {
        const id = document.getElementById("idInput").value;
        const price = document.getElementById("newPriceInput").value;
		const address = "https://localhost:7032/api/Components/" + id + "/" + price;
        console.log("Editing component:", id, price);
		fetch(address, {
			method: "PUT"
		})
		.then(response => {
			console.log('response.status: ', response.status);
			console.log(response);
			if (!response.ok) {
				console.log('Response not ok');
				document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
			} 
		})
		//updateTable();
	  }
	  
	 function updateTable(){
			let address = "https://localhost:7032/api/Components";
			fetch(address, {
				method: "GET"
			})
			.then(response => {
				currentStatus = response.status;
				console.log('response.status: ', response.status);
				if (response.ok){
					//add data to table
					//TODO: add actual data
					//console.log(JSON.stringify(response.json));
					
				}
			})

			.catch((err) => {
				console.log(err);
			});	
	}
	  /*
	  function addTableRow(currentID){
		let address = "https://localhost:7032/api/Components/"
		let currentStatus=0;
			fetch(address+currentID, {
				method: "GET",
			})
			.then(response => {
				currentStatus = response.status;
				console.log('response.status: ', response.status);
				if (response.ok){
					//add data to table
					let table = document.getElementById("componentTable");
					let row = table.insertRow(currentID);
					let cellID = row.insertCell(0);
					let cellName = row.insertCell(1);
					let cellPrice = row.insertCell(2);
					let cellMax = row.insertCell(3);
					//TODO: add actual data
					
					addTableRow(currentID+1);
				}
			})
			.catch((err) => {
				console.log(err);
			});	
			console.log(currentStatus)
	  }
	  */