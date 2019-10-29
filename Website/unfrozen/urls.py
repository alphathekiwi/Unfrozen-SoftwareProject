from django.urls import path

from . import views

urlpatterns = [
    path('', views.index, name='index'),
    path('about/', views.about, name='about'),
    path('whattodo/',views.tags_and_articles_list, name='whattodo'),
    path('by_tag/<str:mytag>/', views.articles_by_tag_list, name = 'articles_by_tag_list'),
    path('article/<str:myarticleID>/', views.article, name = 'article'),
    path('ourteam/',views.ourteam, name='ourteam'),
    path('author/<str:myauthorfullname>/', views.author, name = 'author'),
    #path('/images/<str:myauthorfullname>/', views.author_img, name='author_img'),
]
