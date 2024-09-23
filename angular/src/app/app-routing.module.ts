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
		path: "medias",
		loadChildren: () =>
			import("./medias/medias.module").then((m) => m.MediasModule),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
