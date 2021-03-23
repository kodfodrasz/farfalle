// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require("path");

module.exports = {
  mode: "development",
  entry: "./Farfalle.Fable.fsproj",
  output: {
    path: path.join(__dirname, "./public"),
    filename: "farfalle.js",
  },
  devServer: {
    publicPath: "/",
    contentBase: "./public",
    port: 8080,
  },
  devtool: "inline-cheap-source-map",
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: 'fable-loader',
        }
      }
    ]
  }
}
