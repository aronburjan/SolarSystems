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
    
    updateBottomTable(col);
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
    
	
    //Get selected row/column
    var row = this.parentNode.rowIndex;
    var col = this.cellIndex;
    //Get selected shelf
    var shelf = document.querySelector('#TopDownShelvesTable .selected').cellIndex;

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
					option.value = item.id;
					option.text = item.componentName;
					componentList.add(option);
					
				}); 
			})
			.catch((err) => {
				console.log(err);
			});
}


function updateBottomTable(shelf) {
  console.log("Updating bottom table with data for column " + shelf);

}
