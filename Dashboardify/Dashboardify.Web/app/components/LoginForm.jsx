import React from 'react'
import {findDOMNode} from 'react-dom';
import { withRouter } from 'react-router'

class LoginForm extends React.Component {
	constructor(props, context) {
		super(props, context);

		this.state = {
			loginFailed: false,
			loginFailedMessage: 'Unexpected error occured.',
			submitButtonText: 'Sign in'
		}
	}

	componentDidMount() {
		let {email} = this.refs;

		findDOMNode(email).focus();
	}

	toggleInputs() {
		let {email, password, submit} = this.refs;

		email.disabled = !email.disabled;
		password.disabled = !password.disabled;
		submit.disabled = !submit.disabled;
	}

	handleFormSubmit(e) {
		e.preventDefault();
		let {email, password, submit} = this.refs;

		if (email.value.length > 0 && password.value.length > 0) {

			this.toggleInputs();
			this.setState({
				submitButtonText: 'Signing in...'
			});

			let that = this;

			setTimeout(() => {

				if (email.value === 'maestro@maestro.lt' && password.value === '123') {
					that.props.router.push('/');
				} else {
					this.toggleInputs();

					password.value = '';
					findDOMNode(password).focus();

					this.setState({
						loginFailed: true,
						loginFailedMessage: 'Incorrect email or password.',
						submitButtonText: 'Sign in'
					});
				}
			}, 1000);

		}
	}

	render() {
		let renderErrorMessage = () => {
			if (this.state.loginFailed) {
				return (
					<div className="panel-heading">
						<strong>Oh snap! </strong>
						{this.state.loginFailedMessage}
					</div>
				)
			}
		};

		return (
			<div className={"panel " + (this.state.loginFailed ? "panel-danger" : "panel-default")}>
				{renderErrorMessage()}
				<div className="panel-body">
					<form onSubmit={this.handleFormSubmit.bind(this)}>
						<div className="form-group">
							<label htmlFor="login-email">Email address</label>
							<input type="email" id="login-email" ref="email" className="form-control" placeholder="darth.vader@empire.gov" required/>
						</div>
						<div className="form-group">
							<label htmlFor="login-password">Password</label>
							<input type="password" id="login-password" ref="password" className="form-control" placeholder="IFeelTheForce123" required/>
						</div>
						<button type="submit" ref="submit" className="btn btn-primary">{this.state.submitButtonText}</button>
					</form>
					<hr/>
					<p><a href="">Forgot password</a></p>
					<p>Not a user yet?	<a href="">Register</a></p>
				</div>
			</div>
		)
	}
}

var DecoratedLoginForm = withRouter(LoginForm);

LoginForm.propTypes = {
  router: React.PropTypes.shape({
    push: React.PropTypes.func.isRequired
  }).isRequired
};

export default DecoratedLoginForm;
