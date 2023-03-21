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
          form.setAttribute("onsubmit", "event.preventDefault(); editComponent()");
		  document.getElementById("ModeLabel").innerHTML = "Edit";
        } else {
          // Add mode
          addMode.style.display = "block";
          editMode.style.display = "none";
          form.setAttribute("onsubmit", "event.preventDefault(); addComponent()");
		  document.getElementById("ModeLabel").innerHTML = "Add";
        }
      }

      function addComponent() {
        const name = document.getElementById("nameInput").value;
        const price = document.getElementById("priceInput").value;
        const max = document.getElementById("maxInput").value;
        console.log(JSON.stringify({
		name: name,
		price: price,
		max: max}))
        console.log("Adding component:", name, price, max);
		//TODO: send request
		updateTable();
      }

      function editComponent() {
        const id = document.getElementById("idInput").value;
        const price = document.getElementById("newPriceInput").value;
        console.log("Editing component:", id, price);
		//TODO: send request
		updateTable();
	  }
	  
	  function updateTable(){
			addTableRow(0);
	  }
	  
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
					cellID.innerHTML = "testID";
					cellName.innerHTML = "testName";
					addTableRow(currentID+1);
				}
			})
			.catch((err) => {
				console.log(err);
			});	
			console.log(currentStatus)
	  }
	  