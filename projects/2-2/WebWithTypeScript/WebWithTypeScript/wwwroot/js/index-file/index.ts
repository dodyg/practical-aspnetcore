declare var $, Vue;

$(function () {
    const app = new Vue({
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
});