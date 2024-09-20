import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { MainComponent } from "./main/main.component";

export const routes: Routes = [
	{
		path: "",
		component: MainComponent,
	},
	{
		path: ":itemId",
		loadChildren: () =>
			import("./item-details/item-details.module").then(
				(m) => m.ItemDetailsModule
			),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
