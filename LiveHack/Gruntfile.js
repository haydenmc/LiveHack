/// <binding AfterBuild='default' />
module.exports = function (grunt) {
    // Project configuration
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        less: {
            build: {
                src: 'Content/Less/Site.less',
                dest: 'Content/Style.css'
            }
        },
        typescript: {
            base: {
                src: ['Scripts/Ts/**/*.ts'],
                dest: 'Scripts/Application.js',
                options: {
                    module: 'amd', //or commonjs 
                    target: 'es5', //or es3 
                    basePath: 'Scripts/Ts',
                    sourceMap: false,
                    declaration: false,
                    references: [
                        "Scripts/Typings/**/*.d.ts"
                    ]
                }
            }
        }
    });

    // Default task
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-typescript');

    grunt.registerTask('default', ['less', 'typescript']);
};