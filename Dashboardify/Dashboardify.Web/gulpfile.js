var gulp = require("gulp");
var browserify = require("browserify");
var source = require('vinyl-source-stream');
var concat = require("gulp-concat");
var hbsfy = require('hbsfy');

gulp.task('browserify', function () {
    var b = browserify('./src/app/app.js', { debug: true });

    return b.transform(hbsfy)
        .bundle()
        .pipe(source('app.browserified.js'))
        .pipe(gulp.dest('./public'))
});

