const CopyWebpackPlugin = require('copy-webpack-plugin');
const WorkerPlugin = require('worker-plugin');
const sass = require('node-sass');

module.exports = {
  configureWebpack: function () {
    if (process.env.NODE_ENV === 'production') {
      return {
        plugins: [
          new WorkerPlugin(),
          new CopyWebpackPlugin([{
            from: 'src/lib/abp.js',
            to: 'dist'
          }, {
            from: 'src/assets/img/',
            to: 'img'
          },
          {
            from: 'src/assets/scss/app.scss',
            to: 'css/style.css',
            transform (content, path) {
              const result = sass.renderSync({
                file: path,
                outputStyle: "compressed"
              });
              return result.css.toString();
            },
          }
          ])
        ]
      }
    } else {
      return {
        devtool: 'source-map',
        plugins: [
          new WorkerPlugin(),
          new CopyWebpackPlugin([{
            from: 'src/lib/abp.js',
            to: 'dist'
          }, {
            from: 'src/assets/img/',
            to: 'img'
          },
          {
            from: 'src/assets/scss/app.scss',
            to: 'css/style.css',
            transform (content, path) {
              const result = sass.renderSync({
                file: path,
                outputStyle: "compressed"
              });
              return result.css.toString();
            },
          }
          ])
        ]
      }
    }
  },
}