var path = require('path')
var webpack = require('webpack')

var config = {
  devtool: 'eval-source-map',
  entry: {
    'dist/content/content': './src/content/index.js',
    'dist/background/background': './src/background/index.js',
    'dist/popup/popup': './src/popup/index.js'
  },
  output: {
    path: './',
    filename: '[name].js'
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: "babel-loader",
        query: {
          presets: ['es2015']
        }
      }
    ]
  },
  resolve: {
    root: __dirname,
    modulesDirectories: [
      'node_modules'
    ],
    alias: {},
    extensions: ['', '.js']
  }
}

module.exports = config
