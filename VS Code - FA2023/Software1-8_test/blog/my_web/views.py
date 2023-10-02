from django.shortcuts import render

# Create your views here.
def home (request):
    return render (request, "home.html")

def fun (request):
    return render (request, "fun.html")