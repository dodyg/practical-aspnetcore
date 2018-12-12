"use strict";

var webpack = require("webpack");
module.exports = {
    entry: {
        index: "./wwwroot/scripts/index/app.tsx",
    },

    output: {
        filename: "[name]-bundle.js",
        path: __dirname + '/wwwroot/'
    },

    mode: "development",

    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",

    resolve: {
        modules: ["src", "node_modules"],

        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: [".ts", ".tsx", ".js", ".json"]
    },

    module: {
        // loaders: [
        //     {
        //         test: /\.jsx?$/,
        //         loader: "babel-loader",
        //         exclude: /node_modules/,
        //         query: { presets: ["es2015", "react"] }
        //     }
        // ],

        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
            {
                test: /\.tsx?$/,
                loader: "ts-loader",
            },

            { 
                test: /\.css$/, 
                loader: ['style-loader', 'css-loader'] 
            },

            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            {
                enforce: "pre",
                test: /\.js$/,
                loader: "source-map-loader",
                exclude: [
                    /\/node_modules\//
                ]
            },
            // {
            //     test: /\.tsx?$/,
            //     loader: 'ts-loader',
            //     exclude: /node_modules/
            // },
        ]
    },

    // When importing a module whose path matches one of the following, just
    // assume a corresponding global variable exists and use that instead.
    // This is important because it allows us to avoid bundling all of our
    // dependencies, which allows browsers to cache those libraries between builds.
    externals: {
        //"react": "React",
        //"react-dom": "ReactDOM",
        // "dotnetify": "dotnetify",
        "jquery": "$",
    }
};