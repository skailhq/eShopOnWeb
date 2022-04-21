#!/bin/bash
TAG=v0.0.1

podman build -t registry.digitalocean.com/skail/func/sendemail:$TAG --build-arg feed_name=$FEED_NAME --build-arg feed_user=$FEED_USER --build-arg feed_password=$FEED_PASSWORD  -f Dockerfile_skail .
podman push registry.digitalocean.com/skail/func/sendemail:$TAG
