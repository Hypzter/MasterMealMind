// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var selectedIds = [];
var checkboxes = document.querySelectorAll('.product-checkbox');
function updateSelectedIds() {
	selectedIds = Array.from(checkboxes)
		.filter(function (checkbox) {
			return checkbox.checked;
		})
		.map(function (checkbox) {
			return checkbox.getAttribute('data-grocery-name');
		});

	// Update the hidden input field with selected IDs
	document.getElementById('selectedGroceryNames').value = selectedIds.join(', ');
}

var allChecked = false;
checkboxes.forEach(function (checkbox) {
	checkbox.addEventListener('change', function () {
		var notAllChecked = false;

		checkboxes.forEach(function (checkbox) {
			if (!checkbox.checked) {
				notAllChecked = true;
			}
		});

		allChecked = !notAllChecked; // Update allChecked based on all checkboxes status
		updateSelectedIds();
	});
});


//const getGroceries = (event) => {
//	event.preventDefault()
//	let checkboxes = document.getElementsByClassName("product-checkbox")
//	let boxArray = Array.from(checkboxes)
//	let checkedIngredients = boxArray.filter(e => e.checked == true).map(e => e.dataset.groceryName)
//	console.log(checkedIngredients)
//	document.getElementById('selectedGroceryNames').value = checkedIngredients.join(','); // Joining the array into a comma-separated string
//	// document.getElementById("formSelectedGroceryNames").submit()


//}