const path = require('path');
const webpack = require('webpack');
const CopyPlugin = require('copy-webpack-plugin');
const assets = require('./assets');

module.exports = {
    entry: {
        app: "./app.js",
    },
    output: {
        path: __dirname + "/Assets/lib/",
        filename: "[name].bundle.js"
    },
    plugins: [
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
