const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const CopyPlugin = require('copy-webpack-plugin');
const assets = require('./assets');
var extractPlugin = new ExtractTextPlugin({
    filename: 'css/[name].css'
});

module.exports = {
    entry: {
        app: "./app.js"
    },
    output: {
        path: __dirname + "/Assets/lib/",
        filename: "[name].bundle.js"
    },
    module: {
        rules: [
            {
                test: /\.scss$/,
                use: extractPlugin.extract({
                    use: ['css-loader', 'sass-loader']
                })
            }
        ]
    },
    plugins: [
        extractPlugin,
        new CopyPlugin(
            assets.JS.map(asset => {
                return {
                    from: path.resolve(__dirname, `./node_modules/${asset}`),
                    to: path.resolve(__dirname, './Assets/lib/js/')
                };
            })
        ),
        new CopyPlugin(
            assets.CSS.map(asset => {
                return {
                    from: path.resolve(__dirname, `./node_modules/${asset}`),
                    to: path.resolve(__dirname, './Assets/lib/css/')
                };
            })
        )
    ]
};
