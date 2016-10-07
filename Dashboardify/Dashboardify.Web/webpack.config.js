var path = require('path')
var webpack = require('webpack')
var HtmlWebpackPlugin = require('html-webpack-plugin')

var config = {
  devtool: 'eval-source-map',
  entry: [
    'webpack-hot-middleware/client?reload=true',
    path.join(__dirname, 'app/app.jsx')
  ],
  output: {
    path: path.join(__dirname, '/dist/'),
    filename: '[name].js',
    publicPath: '/'
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: 'app/index.tpl.html',
      inject: 'body',
      filename: 'index.html'
    }),
    new webpack.optimize.OccurenceOrderPlugin(),
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NoErrorsPlugin(),
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify('development')
    })
  ],
  module: {
    loaders: [{
      test: /\.jsx?$/,
      exclude: /node_modules/,
      loader: 'babel',
      query: {
        presets: ['react', 'es2015', 'react-hmre'],
        plugins: ['transform-object-rest-spread']
      }
    }, {
      test: /\.json?$/,
      loader: 'json'
    }, {
      test: /\.css$/,
      loader: 'style!css?modules&localIdentName=[name]---[local]---[hash:base64:5]'
    }, {
      test: /\.(jpe?g|png|gif|svg)$/,
      loader: 'url-loader'
    }, {
      test: /\.scss$/,
      loader: 'style!css!sass'
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
  },
  sassLoader: {
    includePaths: [
      path.resolve(__dirname, './node_modules/bourbon/app/assets/stylesheets/'),
      path.resolve(__dirname, './node_modules/bourbon-neat/app/assets/stylesheets/'),
      path.resolve(__dirname, './node_modules/react-photoswipe/lib/')
    ]
  }
}

module.exports = config
