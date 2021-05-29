// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#CountryId").on("change", function () {
			console.log("outside");
	fetch("/location/CitySelect/" + $(this).val(), {
		method: "GET"
	})
		.then(res => res.text())
		.then(res => {
			$("#CityId").replaceWith(res);
		});
});

function dateString(date) {
	let content = "";
	content += date.getFullYear();
	content += "-";
	content += date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
	content += "-";
	content += date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
	return content;
}
