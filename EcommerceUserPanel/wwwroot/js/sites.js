// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//function change()
//{
//   document.getElementById("myButton1").innerHTML= "Added to cart";
//}
function change() {
    var QuantityBox = React.createClass({
        getInitialState: function () {
            return { value: 1 };
        },
        onDecrement: function (e) {
            if (this.state.value <= 0) return;
            this.setState({ value: --this.state.value });
        },
        onIncrement: function (e) {
            this.setState({ value: ++this.state.value });
        },
        render: function () {
            return (
                <div className="qty-box">
                    <span className="dec" onClick={this.onDecrement} onTouchStart={this.onDecrement}>–</span>
                    <span className="qty">{this.state.value}</span>
                    <span className="inc" onClick={this.onIncrement} onTouchStart={this.onIncrement}>+</span>
                </div>
            );
        }
    });

    React.render(<QuantityBox />, document.body);
}