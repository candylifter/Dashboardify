'use strict'

var path = require('path')
var webpack = require('webpack')
var HtmlWebpackPlugin = require('html-webpack-plugin')
var ExtractTextPlugin = require('extract-text-webpack-plugin')
var StatsPlugin = require('stats-webpack-plugin')

var config = {
  entry: [
    path.join(__dirname, 'app/app.jsx')
  ],
  output: {
    path: path.join(__dirname, '/dist/'),
    filename: '[name]-[hash].min.js',
    publicPath: '/'
  },
  plugins: [
    new webpack.optimize.OccurenceOrderPlugin(),
    new HtmlWebpackPlugin({
      template: 'app/index.tpl.html',
      inject: 'body',
      filename: 'index.html'
    }),
    new ExtractTextPlugin('[name]-[hash].min.css'),
    new webpack.optimize.UglifyJsPlugin({
      compressor: {
        warnings: false,
        screw_ie8: true
      }
    }),
    new StatsPlugin('webpack.stats.json', {
      source: false,
      modules: false
    }),
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV)
    })
  ],
  module: {
    loaders: [{
      test: /\.jsx?$/,
      exclude: /node_modules/,
      loader: 'babel',
      query: {
        presets: ['react', 'es2015'],
        plugins: ['transform-object-rest-spread']
      }
    }, {
      test: /\.json?$/,
      loader: 'json'
    }, {
      test: /\.css$/,
      loader: 'style!css?modules&localIdentName=[name]---[local]---[hash:base64:5]'
    }]
  },
  resolve: {
    root: __dirname,
    modulesDirectories: [
      'node_modules'
    ],
    alias: {
      applicationStyles: 'app/styles/app.scss',
      components: 'app/components/index.jsx',
      containers: 'app/containers/index.jsx',
      actions: 'app/actions/index.jsx',
      reducers: 'app/reducers/index.jsx',
      configureStore: 'app/store/configureStore',
      api: 'app/api/index.jsx',
      routes: 'app/routes.jsx'
    },
    extensions: ['', '.js', '.jsx']
  }
}

module.exports = config
