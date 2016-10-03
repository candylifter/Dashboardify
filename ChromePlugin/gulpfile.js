var gulp = require('gulp')
var concat = require('gulp-concat')
var pump = require('pump')
var uglify = require('gulp-uglify')
var cssmin = require('gulp-cssmin')

var paths = {
  jquery: 'node_modules/jquery/dist/jquery.min.js',
  'css-selector-generator': 'css-selector-generator'
}
