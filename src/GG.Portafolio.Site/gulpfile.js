/// <binding AfterBuild='default-sass, default-js' Clean='clean-sass, clean-js' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
var gulp = require("gulp");
var del = require("del");
var sass = require('gulp-sass')(require('sass'));
var sourcemaps = require('gulp-sourcemaps');
const webpackstream = require('webpack-stream');
const path = require('path');
const webpack = require('webpack');
process.env.NODE_ENV = process.env.NODE_ENV || "production";//"development";

var paths = {
    scripts: [
        "Npm/TypeScript/**/*.js"
        ,"Npm/TypeScript/**/*.ts"
        ,"Npm/TypeScript/**/*.map"
    ]
};
gulp.task("clean-js", function () {    
    return del(["wwwroot/js/**/*"]);
});

gulp.task("default-js", function (done) {
    
    console.log(process.env.NODE_ENV);//NODE_ENV

    if (process.env.NODE_ENV == "development") {
        gulp.src(paths.scripts)
            .pipe(webpackstream({
                mode: "development",
                entry: {
                    home: './Npm/TypeScript/views/Home/index.ts',
                    dealer: './Npm/TypeScript/views/Dealer/index.ts',
                    templateword: './Npm/TypeScript/views/TemplateWord/index.ts',
                    blog: './Npm/TypeScript/views/blog/index.ts',
                    neweditblog: './Npm/TypeScript/views/BlogManagement/newEditBlog.ts',
                },
                devtool: "source-map",                
                module: {
                    rules: [
                        {
                            test: /\.tsx?$/,
                            use: 'ts-loader',
                            exclude: /node_modules/,
                        }
                    ]
                },
                resolve: {
                    extensions: ['.ts', '.js'],
                },
                output: {
                    filename: '[name].js',
                    path: path.resolve(__dirname, 'wwwroot/js'),
                }
            }, webpack))
            .pipe(gulp.dest('wwwroot/js'));
    }
    else {
        gulp.src(paths.scripts)
            .pipe(webpackstream({
                mode: "production",
                entry: {
                    home: './Npm/TypeScript/views/Home/index.ts',
                    dealer: './Npm/TypeScript/views/Dealer/index.ts',
                    templateword: './Npm/TypeScript/views/TemplateWord/index.ts',
                    blog: './Npm/TypeScript/views/blog/index.ts',
                    neweditblog: './Npm/TypeScript/views/BlogManagement/newEditBlog.ts',
                },
                devtool: "hidden-source-map",
                module: {
                    rules: [
                        {
                            test: /\.tsx?$/,
                            use: 'ts-loader',
                            exclude: /node_modules/,
                        }
                    ]
                },
                resolve: {
                    extensions: ['.ts', '.js'],
                },
                output: {
                    filename: '[name].js',
                    path: path.resolve(__dirname, 'wwwroot/js'),
                },
                optimization: {
                    minimize : true
                }
            }, webpack))
            .pipe(gulp.dest('wwwroot/js'));
    }
    
    done();
});

gulp.task("clean-sass", function () {
    return del(["wwwroot/css/**/*"]);
});

gulp.task("default-sass", function (done) {
    if (process.env.NODE_ENV == "development") {
        gulp.src('Npm/scss/**/*.scss')
            .pipe(sourcemaps.init())
            .pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
            .pipe(sourcemaps.write(''))
            .pipe(gulp.dest('wwwroot/css'));
        done();
    }
    else {
        gulp.src('Npm/scss/**/*.scss')            
            .pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))            
            .pipe(gulp.dest('wwwroot/css'));
        done();
    }
    
});