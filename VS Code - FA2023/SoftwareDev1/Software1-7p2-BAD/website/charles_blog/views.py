from django.shortcuts import render

# Create your views here.
def home (request):
    return render (request, "home.html")

def cat_pics (request):
    return render (request, "cat_pics")