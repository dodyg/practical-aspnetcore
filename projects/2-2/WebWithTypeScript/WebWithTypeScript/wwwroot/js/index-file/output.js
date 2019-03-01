$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            message: 'Hello from Vue'
        },
        methods: {
            greet: function () {
                alert('hello human');
            }
        }
    });
    var app2 = new Vue({
        el: '#app2',
        data: {
            warning: 'Party is about to start'
        }
    });
});
//# sourceMappingURL=output.js.map