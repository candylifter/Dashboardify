var webpack = require('webpack');
var path = require('path');

module.exports = {
  entry: [
    'script!jquery/dist/jquery.min.js',
    './app/app.jsx'
  ],
  externals: {
    jquery: 'jQuery'

  },
  plugins: [
    new webpack.ProvidePlugin({
      '$': 'jquery',
      'jQuery': 'jquery'
    })
  ],
  output: {
    path: __dirname+'/public',
    filename: './bundle.js'
  },
  resolve: {
    root: __dirname,
    modulesDirectories: [
      'node_modules',
      './app/components',
      './app/pages',
      './app/api'
    ],
    alias: {
      applicationStyles: 'app/styles/app.scss',
      DashboardifyAPI: 'app/api/DashboardifyAPI.jsx',
      actions: 'app/actions/index.jsx',
      reducers: 'app/reducers/index.jsx',
      configureStore: 'app/store/configureStore.jsx'
    },
    extensions: ['', '.js', '.jsx']
  },
  module: {
    loaders: [
      {
        loader: 'babel-loader',
        query: {
          presets: ['react', 'es2015']
        },
        test: /\.jsx?$/,
        exclude: /(node_modules|bower_components)/
      },
      {
  			test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
  			loader: "url-loader?limit=10000&mimetype=application/font-woff"
  		},
      {
  			test: /\.(ttf|eot|svg|otf)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
  			loader: "file-loader"
  		}
    ]
  },
  sassLoader: {
    includePaths: [
      path.resolve(__dirname, './node_modules/bootstrap-sass/assets/stylesheets'),
      path.resolve(__dirname, './node_modules/bootstrap-sass'),
      path.resolve(__dirname, './node_modules/bourbon/app/assets/stylesheets/'),
      path.resolve(__dirname, './node_modules/bourbon-neat/app/assets/stylesheets/'),

      // path.resolve(__dirname, './node_modules/foundation-sites/scss')
    ]
  },
  devtool: 'cheap-module-eval-source-map'
};
