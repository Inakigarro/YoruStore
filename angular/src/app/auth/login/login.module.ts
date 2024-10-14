import { NgModule } from "@angular/core";
import { LoginComponent } from "./login.component";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { AppButtonModule } from "@root/components";
import { CommonModule } from "@angular/common";

@NgModule({
	declarations: [LoginComponent],
	imports: [
		CommonModule,
		MatCardModule,
		MatFormFieldModule,
		MatInputModule,
		AppButtonModule,
	],
	exports: [LoginComponent],
})
export class LoginModule {}
