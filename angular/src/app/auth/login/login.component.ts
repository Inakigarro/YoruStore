import { Component, OnInit } from "@angular/core";
import { AuthService } from "../auth.service";
import { LoginService } from "../login.service";

@Component({
	selector: "login",
	templateUrl: "./login.component.html",
	styleUrl: "./login.component.scss",
})
export class LoginComponent implements OnInit {
	constructor(private loginService: LoginService) {}

	public ngOnInit(): void {}
}
