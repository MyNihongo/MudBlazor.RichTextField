const { src, dest, series } = require("gulp");
const minifyCss = require("gulp-clean-css");
const minifyJs = require("gulp-minify");
const rename = require("gulp-rename");

const baseName = "MudBlazor.RichTextField";

function css() {
	return src("Resources/*.css")
		.pipe(minifyCss())
		.pipe(rename({ basename: baseName, extname: ".min.css" }))
		.pipe(dest("wwwroot"));
}

function js() {
	return src("Resources/*.js")
		.pipe(minifyJs())
		.pipe(rename({ basename: baseName, extname: ".min.js" }))
		.pipe(dest("wwwroot"));
}

exports.default = series(css, js);
