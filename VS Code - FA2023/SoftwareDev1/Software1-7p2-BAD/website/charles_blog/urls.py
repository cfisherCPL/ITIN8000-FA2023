from django.urls import path
from . import views

urlpatterns = [ 
    path('home/', views.home, name='home'),
	path('cat_pics/', views.cat_pics, name='cat_pics'),
]