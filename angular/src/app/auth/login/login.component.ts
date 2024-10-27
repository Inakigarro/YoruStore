import { Component, OnInit } from "@angular/core";
import { AuthService } from "../auth.service";
import { Button } from "@root/components/models";
import { createAction } from "@ngrx/store";
import { AuthActions } from "../state/auth.actions";
import { FormControl, FormGroup, Validators } from "@angular/forms";

const dummyAction = createAction("jaja");

@Component({
	selector: "login",
	templateUrl: "./login.component.html",
	styleUrl: "./login.component.scss",
})
export class LoginComponent implements OnInit {
	public loginForm: FormGroup;

	public cancelButton: Button = {
		type: "flat",
		label: "Cancelar",
		icon: "",
		action: dummyAction(),
	};
	constructor(private service: AuthService) {
		this.loginForm = new FormGroup({
			userName: new FormControl<string>("", [Validators.required]),
			password: new FormControl<string>("", [Validators.required]),
		});
	}

	public ngOnInit(): void {}

	public onLoginButtonClicked() {
		let userName = this.loginForm.controls["userName"].value;
		let password = this.loginForm.controls["password"].value;
		this.service.dispatch(
			AuthActions.logInButtonClicked({
				login: {
					userName,
					password,
				},
			})
		);
	}

	public onCancelButtonClicked() {
		this.loginForm.controls["userName"].setValue("");
		this.loginForm.controls["password"].setValue("");
	}
}
