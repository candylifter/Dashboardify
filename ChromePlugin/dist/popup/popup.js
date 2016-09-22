/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports) {

	eval("'use strict';\n\nfunction greeter() {\n  console.log('hello');\n}\n\nmodule.exports = greeter;//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9zcmMvcG9wdXAvaW5kZXguanM/OTk0MiJdLCJuYW1lcyI6WyJncmVldGVyIiwiY29uc29sZSIsImxvZyIsIm1vZHVsZSIsImV4cG9ydHMiXSwibWFwcGluZ3MiOiI7O0FBQUEsU0FBU0EsT0FBVCxHQUFvQjtBQUNsQkMsVUFBUUMsR0FBUixDQUFZLE9BQVo7QUFDRDs7QUFFREMsT0FBT0MsT0FBUCxHQUFpQkosT0FBakIiLCJmaWxlIjoiMC5qcyIsInNvdXJjZXNDb250ZW50IjpbImZ1bmN0aW9uIGdyZWV0ZXIgKCkge1xyXG4gIGNvbnNvbGUubG9nKCdoZWxsbycpXHJcbn1cclxuXHJcbm1vZHVsZS5leHBvcnRzID0gZ3JlZXRlclxyXG5cblxuXG4vKiogV0VCUEFDSyBGT09URVIgKipcbiAqKiAuL3NyYy9wb3B1cC9pbmRleC5qc1xuICoqLyJdLCJzb3VyY2VSb290IjoiIn0=");

/***/ }
/******/ ]);