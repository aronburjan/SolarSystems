updateComponentList();

// Get all the cells of the table
var cells = document.querySelectorAll('#TopDownShelvesTable td');

for (var i = 0; i < cells.length; i++) {
  // Add a click event listener to the cell
  cells[i].addEventListener('click', function() {
	  
	//Deselect column
    var selected = document.querySelectorAll('.selected');
    if (selected) {
      for (var j = 0; j < selected.length; j++) {
        selected[j].classList.remove('selected');
      }
    }
	
    //Select column
    var col = this.cellIndex;
    var cellsInCol = document.querySelectorAll('#TopDownShelvesTable td:nth-child(' + (col + 1) + ')');
    for (var k = 0; k < cellsInCol.length; k++) {
      cellsInCol[k].classList.add('selected');
    }
    
    updateBottomTable(col+1);
  });
}

var selectedCells = document.querySelectorAll('#SelectedShelfTable td');

for (var l = 0; l < selectedCells.length; l++) {
  selectedCells[l].addEventListener('click', function() {
	  
    //Deselect cell
    var selected = document.querySelectorAll('#SelectedShelfTable .selected');
    if (selected) {
      for (var m = 0; m < selected.length; m++) {
        selected[m].classList.remove('selected');
      }
    }
	
    //Select cell
    this.classList.add('selected');

  });
}


function updateComponentList(){
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
				let componentList = document.getElementById("componentList");
				//clear component list
				componentList.innerHTML = '';
				//add each component to list
				data.forEach(item =>{
					let option = document.createElement("option");
					option.value = item.componentName;
					option.text = '(' + item.id + ') ' + item.componentName;
					componentList.add(option);
					
				}); 
			})
			.catch((err) => {
				console.log(err);
			});
}


function updateBottomTable(shelf) {
  console.log("Updating bottom table with data for shelf no. " + shelf);
  let table = document.getElementById("SelectedShelfTable");
  //Clear table
	var r=0;
	while(row=table.rows[r++])
	{
	var c=0;
		while(cell=row.cells[c++])
		{
			cell.innerHTML = '('+r+','+c+')'+' <br> (Empty)';
		}
	}

	let address = "https://localhost:7032/api/Containers";
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
				data.forEach(item =>{
					let table = document.getElementById("SelectedShelfTable");

					//Fill table 
					//Only check if container isn't empty and shelf matches
					if ((item.quantityInContainer > 0) && (item.containerNumber == shelf)) {
						var r=0;
						while(row=table.rows[r++])
						{
							var c=0;
							while(cell=row.cells[c++])
							{
							
								//console.log(shelf + ' ' + item.containerNumber + ' ' + r + ' ' + item.containerRow + ' ' + c + ' ' + item.containerColumn);
							
								//if cell coords match container coords, add text to cell
								if ((item.containerRow == r) && (item.containerColumn == c)){
									cell.innerHTML='('+r+','+c+')'+' <br> ID:(' + item.componentId + ') <br> Quantity:' + item.quantityInContainer ;
								} 
							}
						}
					}
					
				}); 
			})
			.catch((err) => {
				console.log(err);
			});	
}

function addComponent(){
	try {
		let shelf = document.querySelector('#TopDownShelvesTable .selected').cellIndex +1;
		let row = document.querySelector('#SelectedShelfTable .selected').parentNode.rowIndex +1;
		let col = document.querySelector('#SelectedShelfTable .selected').cellIndex +1;
		console.log(shelf, row, col);
	
		let quantity = document.getElementById("quantityInput").value;
		let select = document.getElementById("componentList");
		let component = select.options[select.selectedIndex].value;
		console.log(quantity, component);
	
	
	const address = "https://localhost:7032/api/Components/" + component + "/" + row + "/" + col + "/" + shelf + "/" + quantity  
		//send POST request
		fetch(address, {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
			},

		})
		.then(response => {
			console.log('response.status: ', response.status);
			console.log(response);
			if (!response.ok) {
				console.log('Response not ok');
				document.getElementById("errorMessage").innerHTML = "HTTP ERROR: " + response.status;
			} else {
				updateBottomTable(shelf);
			}
		})
		
	} catch (error) {
		document.getElementById("errorMessage").innerHTML = 'Please select a container to edit!';
		//console.error(error);
	}
}

function listMissing(){
	missingList = document.getElementById("missingList");
	const address = "https://localhost:7032/api/Projects/project/missingcomponents"
	missingList.innerHTML = "";
	
	fetch(address, {
			method: "GET",
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
					missingList.innerHTML += "(" + item.id + ") " + item.componentName + " <br>"; 
				});
			})
			.catch((err) => {
				console.log(err);
			});	

	
}