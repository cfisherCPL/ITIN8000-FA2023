from django.urls import include, path
from . import views

urlpatterns = [ 
    path('home/', include(views.home, name='home')),
    path('resources/', include(views.resources, name='resources')),
]