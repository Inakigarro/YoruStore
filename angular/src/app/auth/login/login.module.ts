import { NgModule } from "@angular/core";
import { LoginComponent } from "./login.component";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { AppButtonModule } from "@root/components";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
	declarations: [LoginComponent],
	imports: [
		CommonModule,
		MatCardModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
		AppButtonModule,
		ReactiveFormsModule,
	],
	exports: [LoginComponent],
})
export class LoginModule {}
