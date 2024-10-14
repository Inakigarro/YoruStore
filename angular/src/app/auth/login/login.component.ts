import { Component, OnInit } from "@angular/core";
import { AuthService } from "../auth.service";
import { LoginService } from "../login.service";
import { Button } from "@root/components/models";
import { createAction } from "@ngrx/store";

const dummyAction = createAction("jaja");

@Component({
	selector: "login",
	templateUrl: "./login.component.html",
	styleUrl: "./login.component.scss",
})
export class LoginComponent implements OnInit {
	public loginButton: Button = {
		type: "raised",
		label: "Iniciar Sesion",
		icon: "",
		action: dummyAction(),
	};

	public cancelButton: Button = {
		type: "flat",
		label: "Cancelar",
		icon: "",
		action: dummyAction(),
	};
	constructor(private loginService: LoginService) {}

	public ngOnInit(): void {}
}
