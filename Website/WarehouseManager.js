  // Get all the cells of the table
  var cells = document.querySelectorAll('#TopDownShelvesTable td');

  // Loop through each cell
  for (var i = 0; i < cells.length; i++) {
    // Add a click event listener to the cell
    cells[i].addEventListener('click', function() {
      // Remove the yellow color from any previously selected cell
      var selected = document.querySelector('.selected');
      if (selected) {
        selected.classList.remove('selected');
      }
      
      // Add the yellow color to the clicked cell
      this.classList.add('selected');
      
      // Get the row and column of the clicked cell
      var row = this.parentNode.rowIndex;
      var col = this.cellIndex;
      
      // Call the MyFunction() with the row and column as arguments
      MyFunction(row, col);
    });
  }
  
  // Example function that logs the row and column to the console
  //todo: get data from server, fill out bottom table with data
  function MyFunction(row, col) {
    console.log("Selected cell is in row " + row + ", column " + col);
  }
