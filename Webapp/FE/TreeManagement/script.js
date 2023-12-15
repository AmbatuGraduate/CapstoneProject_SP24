document.querySelector(".jsFilter").addEventListener("click", function () {
  document.querySelector(".filter-menu").classList.toggle("active");
});

document.querySelector(".grid").addEventListener("click", function () {
  document.querySelector(".list").classList.remove("active");
  document.querySelector(".grid").classList.add("active");
  document.querySelector(".products-area-wrapper").classList.add("gridView");
  document
    .querySelector(".products-area-wrapper")
    .classList.remove("tableView");
});

document.querySelector(".list").addEventListener("click", function () {
  document.querySelector(".list").classList.add("active");
  document.querySelector(".grid").classList.remove("active");
  document.querySelector(".products-area-wrapper").classList.remove("gridView");
  document.querySelector(".products-area-wrapper").classList.add("tableView");
});

var modeSwitch = document.querySelector('.mode-switch');
modeSwitch.addEventListener('click', function () {
  document.documentElement.classList.toggle('light');
  modeSwitch.classList.toggle('active');
});


document.getElementById('openModalBtn').onclick = function () {
  document.getElementById('myModal').style.display = "block";
};

document.getElementsByClassName('close')[0].onclick = function () {
  document.getElementById('myModal').style.display = "none";
};

document.getElementById('yesBtn').onclick = function () {
  document.getElementById('myModal').style.display = "none";
};

document.getElementById('noBtn').onclick = function () {
  document.getElementById('myModal').style.display = "none";
};

window.onclick = function (event) {
  if (event.target == document.getElementById('myModal')) {
    document.getElementById('myModal').style.display = "none";
  }
};

function saveTree() {
  // Add your logic to save the tree data here
  alert("Tree saved!");
  // You may want to close the modal after saving
  $('#addTreeModal').modal('hide');
}